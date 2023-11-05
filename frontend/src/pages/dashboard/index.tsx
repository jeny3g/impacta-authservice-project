import { ReactNode } from "react";

import { GetServerSidePropsContext } from "next";

import { DashBoardLayout } from "@/app/layouts/DashboardLayout";
import { validateToken } from "@/utils/authUtils";

export async function getServerSideProps(context: GetServerSidePropsContext) {
  return validateToken(context);
}

function Dashboard() {
  return <div>Hello, Dashboard Page!</div>;
}

Dashboard.getLayout = function getLayout(page: ReactNode) {
  return <DashBoardLayout>{page}</DashBoardLayout>;
};

export default Dashboard;
