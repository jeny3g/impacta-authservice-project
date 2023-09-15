import React, { useState } from "react";

import { Avatar, Button, Menu, MenuItem } from "@mui/material";
import { useRouter } from "next/router";

import useAuth from "@/app/hooks/useAuth";

export const UserMenu = () => {
  const router = useRouter();
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const { user, signOut } = useAuth();

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleProfile = () => {
    handleClose();
    router.push("/profile");
  };

  const handleSignOut = () => {
    signOut();
    handleClose();
    router.push("/login");
  };

  return (
    <>
      <Button
        id="basic-button"
        color="secondary"
        aria-controls={open ? "basic-menu" : undefined}
        aria-haspopup="true"
        aria-expanded={open ? "true" : undefined}
        onClick={handleClick}
        sx={{ color: "#000" }}
      >
        <Avatar src="" sx={{ width: 32, height: 32 }}>
          {user?.unique_name[0]}
        </Avatar>
      </Button>
      <Menu
        id="basic-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        MenuListProps={{ "aria-labelledby": "basic-button" }}
      >
        <MenuItem onClick={handleProfile}>Profile</MenuItem>
        <MenuItem onClick={handleSignOut}>Logout</MenuItem>
      </Menu>
    </>
  );
};
