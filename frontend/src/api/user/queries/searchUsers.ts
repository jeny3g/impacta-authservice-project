import { api } from "@/lib/axios";

export const searchUsers = async (
  searchQuery: SearchUsersQuery = {},
  pageRequest: PageRequestDto = {},
  ordination: OrdinationDto = {},
) => {
  const params = {
    ...searchQuery,
    ...pageRequest,
    ...ordination,
  };

  const response = await api.get<PageResponse<UserSearchViewModel>>(
    "api/Users",
    { params },
  );

  return response.data;
};
