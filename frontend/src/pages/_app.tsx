import { QueryClient, QueryClientProvider } from "react-query";
import { ToastContainer } from "react-toastify";

import { AuthProvider } from "@/app/providers/AuthProvider";
import { AppPropsWithLayout } from "@/types/AppPropsWithLayout";

import "react-toastify/dist/ReactToastify.css";
import "../styles/global.css";

const queryClient = new QueryClient();

function MyApp({ Component, pageProps }: AppPropsWithLayout) {
  const getLayout = Component.getLayout ?? ((page) => page);

  const Layout = <Component {...pageProps} />;

  return (
    <>
      <QueryClientProvider client={queryClient}>
        <ToastContainer />

        <AuthProvider>{getLayout(Layout)}</AuthProvider>
      </QueryClientProvider>
    </>
  );
}

export default MyApp;
