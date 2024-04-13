// Import PageRoutes dynamically
import PageRoutes from "./Routes.jsx";
import React from "react";
import { Route } from "react-router-dom";

// function to generate sitemap html (jsx)
export function SitemapHTML() {
  const routes = getRoutes();
  const domain = import.meta.env.VITE_REACTAPP_URL;

  const urlEntries = routes
    .map((route) => {
      const url = `${domain}${route.path}`;
      const displayName = route.path.substring(1); // remove leading slash

      // Skip if displayName is empty
      if (!displayName) {
        return null;
      }

      return (
        <li key={url} style={{ margin: "10px" }}>
          <a href={url} style={{ color: "blue", textDecoration: "none" }}>
            {displayName}
          </a>
        </li>
      );
    })
    .filter(Boolean); // Remove null entries;;

  return (
    <>
      <h1>SiteMap</h1>
      <ul style={{ listStyleType: "none", padding: 0 }}>{urlEntries}</ul>
    </>
  );
}

function getRoutes() {
  console.log("getting routes");
  // Extract routes
  const routes = React.Children.toArray(PageRoutes().props.children)
    .filter((child) => React.isValidElement(child) && child.type === Route)
    .map((route) => ({
      path: route.props.path,
    }));

  return routes;
}
