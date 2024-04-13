import { NavbarHTML } from "./components/visual/headings/Navbar.jsx";
import { FooterHTML } from "./components/visual/footers/Footer.jsx";
import "./styles/App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { useEffect } from "react";

// react-auth-kit
import { AuthProvider } from "react-auth-kit";
import {
  createRefreshApi,
  useRefreshBearerToken,
} from "./components/logic/RefreshApi.jsx";

// Import pageview tracking component
import { PageViewTracker } from "./components/logic/tracking/PageViewTracker.jsx";

// importing the pages
import PageRoutes from "./components/logic/Routes.jsx";

// importing components from react-router-dom package
import { BrowserRouter as Router } from "react-router-dom";

// creating a new query client, which will be used to manage the state of the application and fetch data
const queryClient = new QueryClient();

const RefreshTokenComponent = () => {
  const refreshBearerToken = useRefreshBearerToken();

  useEffect(() => {
    refreshBearerToken();
  }, []);

  return null;
};

const App = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <AuthProvider
        authType={"cookie"}
        authName={"_auth"}
        cookieDomain={window.location.hostname}
        cookieSecure={window.location.protocol === "https:"}
        refresh={createRefreshApi}
      >
        <RefreshTokenComponent />
        <Router>
          <PageViewTracker />
          <NavbarHTML />
          <div id="main-content">
            <PageRoutes />
          </div>
          <FooterHTML />
        </Router>
      </AuthProvider>
    </QueryClientProvider>
  );
};

export default App;
