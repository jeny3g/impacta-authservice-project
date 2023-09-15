import { ReactNode, useEffect, useState } from "react";

import jsCookie from "js-cookie";
import jwt from "jsonwebtoken";

import { AuthContext } from "@/app/contexts/AuthContext";

export type AuthProviderProps = {
  children: ReactNode;
};

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [user, setUser] = useState<DecodedUser | null>(null);

  useEffect(() => {
    const token = jsCookie.get("jwt_token");
    if (token) {
      const decodedUser = jwt.decode(token) as DecodedUser;
      setUser(decodedUser);
    }
  }, []);

  const signIn = async (response: AuthenticateSessionViewModel) => {
    if (response && response.token) {
      jsCookie.set("jwt_token", response.token, { secure: true });

      const decodedUser = jwt.decode(response.token) as DecodedUser;

      setUser(decodedUser);
    }
  };

  const signOut = () => {
    jsCookie.remove("jwt_token");
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ user, signIn, signOut }}>
      {children}
    </AuthContext.Provider>
  );
};
