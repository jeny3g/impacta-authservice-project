import { useMutation } from "react-query";

import { api } from "@/lib/axios";

const forgetPassword = async (data: ForgotPasswordCommand) => {
  const response = await api.post<CreateSuccess>("api/Passwords/forgot", data);
  return response.data;
};

export const useForgetPasswordMutation = () => {
  return useMutation(forgetPassword, {
    onSuccess: (data) => {
      console.log(data);
    },
    onError: (error) => {
      console.log(error);
    },
  });
};
