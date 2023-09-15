import { useMutation } from "react-query";

import { api } from "@/lib/axios";

const createUser = async (data: CreateUserCommand) => {
  const response = await api.post<AuthenticateSessionViewModel>(
    "api/Users/create",
    data,
  );
  return response.data;
};

export const useCreateUserMutation = () => {
  return useMutation(createUser, {
    onSuccess: (data) => {
      console.log(data);
    },
    onError: (error) => {
      console.log(error);
    },
  });
};
