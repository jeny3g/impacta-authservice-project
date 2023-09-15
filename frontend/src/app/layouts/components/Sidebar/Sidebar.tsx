import React from "react";

import {
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Divider,
  Toolbar,
  Avatar,
} from "@mui/material";
import Link from "next/link";
import { usePathname } from "next/navigation";

import useAuth from "@/app/hooks/useAuth";

import styles from "./sidebar.module.css";

type MenuItem = {
  text: string;
  href: string;
};

const menuItems: MenuItem[] = [
  { text: "Dashboard", href: "/dashboard" },
  { text: "Usúarios", href: "/usuario" },
];

interface SidebarProps {
  closeDrawer: () => void;
}

export const Sidebar: React.FC<SidebarProps> = ({ closeDrawer }) => {
  const { user } = useAuth();
  const pathname = usePathname();

  const renderListItemButton = (text: string, href: string) => {
    const isActive = pathname ? pathname.includes(href) : false;

    return (
      <Link key={text} href={href} passHref>
        <ListItemButton
          onClick={closeDrawer}
          sx={{
            color: "#000",
          }}
          style={{
            color: isActive ? "#fff" : "#000",
            backgroundColor: isActive ? "rgb(94, 91, 99)" : "",
          }}
        >
          <ListItemIcon></ListItemIcon>
          <ListItemText primary={text} />
        </ListItemButton>
      </Link>
    );
  };

  return (
    <div>
      <Toolbar
        sx={{
          backgroundColor: "rgb(77, 74, 83)",
        }}
      />
      <div className={styles.photo}>
        <Avatar src="" sx={{ width: 100, height: 100 }}>
          {user?.unique_name[0]}
        </Avatar>
        <h3>{user?.unique_name}</h3>
      </div>
      <Divider />

      <List>
        <ListItem disablePadding>
          <ListItemButton>
            <ListItemIcon></ListItemIcon>
            <ListItemText primary="Menu" />
          </ListItemButton>
        </ListItem>
      </List>
      <Divider />
      <List>
        {menuItems.map(({ text, href }) => renderListItemButton(text, href))}
      </List>
    </div>
  );
};
