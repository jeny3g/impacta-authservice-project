import React from "react";

import MenuIcon from "@mui/icons-material/Menu";
import { AppBar, Grid, IconButton, Toolbar, Typography } from "@mui/material";
import Image from "next/image";
import Link from "next/link";

import useAuth from "@/app/hooks/useAuth";
import { drawerWidth } from "@/utils/constants";

import styles from "./header.module.css";
import { UserMenu } from "../UserMenu/UserMenu";

interface HeaderProps {
  handleDrawerToggle: () => void;
}

export const Header: React.FC<HeaderProps> = ({ handleDrawerToggle }) => {
  const { user } = useAuth();

  return (
    <AppBar
      position="fixed"
      sx={{
        backgroundColor: "rgb(77, 74, 83)",
        width: { sm: `calc(100% - ${drawerWidth}px)` },
        ml: { sm: `${drawerWidth}px` },
      }}
    >
      <Toolbar>
        <IconButton
          color="inherit"
          aria-label="open drawer"
          edge="start"
          onClick={handleDrawerToggle}
          sx={{ mr: 2, display: { sm: "none" } }}
        >
          <MenuIcon />
        </IconButton>
        <Grid
          container
          justifyContent="space-between"
          alignItems="center"
          paddingRight={6}
        >
          <Typography variant="h6" noWrap component="div">
            <div className={styles.logo}>
              <Link href="/dashboard">
                <Image
                  src={"/name-logo.png"}
                  alt="Logo"
                  height={34}
                  width={130}
                />
              </Link>
            </div>
          </Typography>

          <div className={styles.user}>
            <UserMenu />
            <p>{user?.unique_name}</p>
          </div>
        </Grid>
      </Toolbar>
    </AppBar>
  );
};
