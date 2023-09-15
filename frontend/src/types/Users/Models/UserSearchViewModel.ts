type UserSearchViewModel = ETrackerViewModel & {
  name: string;
  email: string;
  active: boolean;
};

type MappedUserSearchViewModel = ETrackerViewModel & {
  name: string;
  email: string;
  active: string;
};
