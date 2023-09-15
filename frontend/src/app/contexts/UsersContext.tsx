import { createContext } from "react";

interface UsersContextProps {
  users: MappedUserSearchViewModel[];
  isLoading: boolean;
  applyFilter: (filter: SearchUsersQuery) => void;
}

export const UsersContext = createContext<UsersContextProps | undefined>(
  undefined,
);
