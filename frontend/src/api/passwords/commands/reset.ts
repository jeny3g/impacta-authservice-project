import { useMutation } from "react-query";

import { api } from "@/lib/axios";

const resetPassword = async (data: ResetPasswordCommand) => {
  const response = await api.post<CreateSuccess>("api/Passwords/reset", data);
  return response.data;
};

export const useResetPasswordMutation = () => {
  return useMutation(resetPassword, {
    onSuccess: (data) => {
      console.log(data);
    },
    onError: (error) => {
      console.log(error);
    },
  });
};
