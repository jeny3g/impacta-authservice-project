import { useEffect, useState } from "react";

import { useQuery } from "react-query";

import { searchUsers } from "@/api/user/queries/searchUsers";

import { UsersContext } from "../contexts/UsersContext";

type UsersProviderProps = {
  children: React.ReactNode;
};

export const UsersProvider: React.FC<UsersProviderProps> = ({ children }) => {
  const [users, setUsers] = useState<MappedUserSearchViewModel[]>([]);
  const [filter, setFilter] = useState<SearchUsersQuery>({});

  const { data, isLoading } = useQuery(["usuarios", filter], () =>
    searchUsers(filter),
  );

  useEffect(() => {
    if (data) {
      const mappedUsers = data.content.map((user) => ({
        ...user,
        active: user.active ? "Sim" : "Não",
      }));

      setUsers(mappedUsers);
    }
  }, [data]);

  const applyFilter = (newFilter: SearchUsersQuery) => {
    setFilter(newFilter);
  };

  return (
    <UsersContext.Provider
      value={{
        users,
        isLoading,
        applyFilter,
      }}
    >
      {children}
    </UsersContext.Provider>
  );
};
