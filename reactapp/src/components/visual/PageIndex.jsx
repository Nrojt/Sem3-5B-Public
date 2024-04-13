import React, { useEffect, useState } from "react";
import { ListGroup } from "react-bootstrap";
import { useTranslation } from "react-i18next";

// Page index component, used to create a list of links to sections on a page.
// this requires every section to have an id and a heading.
const PageIndex = () => {
  // using the translation hook to check when the language changes
  const { i18n } = useTranslation();

  // Initialize indexItems state to an empty array
  const [indexItems, setIndexItems] = useState([]);

  useEffect(() => {
    // Get all section elements on the page and convert to an array
    const sections = Array.from(document.querySelectorAll("section"));

    // Map over the sections array to create a new array of index items
    const newItems = sections
      .map((section) => {
        // Get the id of the section
        const sectionId = section.id;

        // Get the title of the section (the text content of the first heading element in the section)
        const sectionTitle = section.querySelector("h1, h2, h3, h4, h5, h6");

        // If the section has a title, return an object representing the index item
        if (sectionTitle) {
          return {
            id: sectionId,
            title: sectionTitle.textContent,
            level: parseInt(sectionTitle.tagName.charAt(1)), // Get the heading level by converting the second character of the tag name to an integer. Kinda hacky, but it works.
          };
        }

        // If the section doesn't have a title, return null
        return null;
      })
      .filter(Boolean); // Filtering out the null values, which are represented as false in JavaScript
    // When called as a function, Boolean converts its argument to a boolean value. Null is converted to false, and (most) other values are converted to true.

    // Update the indexItems state with the new array of index items
    setIndexItems(newItems);
  }, [i18n.language]); // run when the component mounts and when the language changes

  // Rendering the list of index items, if there are any. Otherwise, render nothing.
  return indexItems.length > 0 ? (
    <ListGroup style={listGroupStyles}>
      {indexItems.map((item) => (
        <ListGroup.Item
          key={item.id}
          action
          href={`#${item.id}`}
          style={{
            color: "white",
            backgroundColor: "#131B56",
            paddingLeft: `${item.level}em`,
          }} // Indent the item based on its level
        >
          {item.title}
        </ListGroup.Item>
      ))}
    </ListGroup>
  ) : null;
};
const listGroupStyles = {
  width: "fit-content", // Adjust the width as needed
  backgroundColor: "#f8f9fa",
  border: "1px solid #dee2e6",
  borderRadius: "30px",
  marginBottom: "20px",
  marginLeft: "10%",
};
export default PageIndex;
