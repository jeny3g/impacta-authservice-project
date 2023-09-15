import jwt from "jsonwebtoken";
import { GetServerSidePropsContext } from "next";

const redirectToLogin = {
  redirect: {
    destination: "/login",
    permanent: false,
  },
};

export async function validateToken(context: GetServerSidePropsContext) {
  const token = context.req.cookies["jwt_token"];
  const secretKey = "aVeryLongSecretKeyThatIsAtLeastSixteenCharacters";

  if (!token) {
    return redirectToLogin;
  }

  try {
    const decoded = await jwt.verify(token, secretKey);
    return {
      props: { decoded },
    };
  } catch (error) {
    return redirectToLogin;
  }
}
