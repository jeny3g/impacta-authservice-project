import { useMutation } from "react-query";

import { api } from "@/lib/axios";

const signInUser = async (data: AuthenticateSessionCommand) => {
  const response = await api.post<AuthenticateSessionViewModel>(
    "api/Sessions/authenticate",
    data,
  );
  return response.data;
};

export const useSignInUserMutation = () => {
  return useMutation(signInUser, {
    onSuccess: (data) => {
      console.log(data);
    },
    onError: (error) => {
      console.log(error);
    },
  });
};
