import * as React from "react";

import { Container } from "@mui/material";
import Box from "@mui/material/Box";
import CssBaseline from "@mui/material/CssBaseline";
import Drawer from "@mui/material/Drawer";
import Toolbar from "@mui/material/Toolbar";

import { Header } from "@/app/layouts/components/Header/Header";
import { drawerWidth } from "@/utils/constants";

import { Sidebar } from "./components/Sidebar/Sidebar";

const drawerStyles = {
  "& .MuiDrawer-paper": {
    boxSizing: "border-box",
    width: drawerWidth,
  },
};

interface DashBoardLayoutProps {
  window?: () => Window;
  children: React.ReactNode;
}

export function DashBoardLayout({ children, window }: DashBoardLayoutProps) {
  const [mobileOpen, setMobileOpen] = React.useState(false);

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen);
  };

  const container =
    window !== undefined ? () => window().document.body : undefined;

  return (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />
      <Header handleDrawerToggle={handleDrawerToggle} />
      <Box
        component="nav"
        sx={{ width: { sm: drawerWidth }, flexShrink: { sm: 0 } }}
        aria-label="mailbox folders"
      >
        <Drawer
          container={container}
          variant="temporary"
          open={mobileOpen}
          onClose={handleDrawerToggle}
          ModalProps={{ keepMounted: true }}
          sx={{
            display: { xs: "block", sm: "none" },
            ...drawerStyles,
          }}
        >
          <Sidebar closeDrawer={handleDrawerToggle} />
        </Drawer>
        <Drawer
          variant="permanent"
          open={true}
          sx={{
            display: { xs: "none", sm: "block" },
            ...drawerStyles,
          }}
        >
          <Sidebar closeDrawer={handleDrawerToggle} />
        </Drawer>
      </Box>
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          p: 3,
          width: {
            xs: mobileOpen ? `calc(100% - ${drawerWidth}px)` : "100%",
            sm: `calc(100% - ${drawerWidth}px)`,
          },
        }}
      >
        <Toolbar />
        <Container fixed>{children}</Container>
      </Box>
    </Box>
  );
}
