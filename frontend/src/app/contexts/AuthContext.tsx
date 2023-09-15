import { createContext } from "react";

export interface AuthContextType {
  user: DecodedUser | null;
  signIn: (response: AuthenticateSessionViewModel) => void;
  signOut: () => void;
}

export const AuthContext = createContext<AuthContextType | undefined>(
  undefined,
);
