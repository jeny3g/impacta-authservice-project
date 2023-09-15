import { useMutation } from "react-query";

import { api } from "@/lib/axios";

const verifyUser = async (token: string) => {
  const response = await api.put<CreateSuccess>("api/Users/verify", { token });
  return response.data;
};

export const useVerifyUserMutation = () => {
  return useMutation(verifyUser, {
    onSuccess: (data) => {
      console.log(data);
    },
    onError: (error) => {
      console.log(error);
    },
  });
};
