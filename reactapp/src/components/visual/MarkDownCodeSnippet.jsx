import Markdown from "react-markdown";
import rehypeHighlight from "rehype-highlight";
import PropTypes from "prop-types";

// importing css for the background of the code snippet
import "../../styles/MarkDownCodeSnippet.css";

// importing syntax highlighting
import hljs from "highlight.js";
// registering javascript
import javascript from "highlight.js/lib/languages/javascript";
hljs.registerLanguage("javascript", javascript);
// registering json
import json from "highlight.js/lib/languages/json";
hljs.registerLanguage("json", json);
// applying a css theme for syntax highlighting
import "highlight.js/styles/atom-one-dark.css";

const MarkDownCodeSnippet = ({ markdown, markdownTitle }) => {
  return (
    <div className="markdown-body">
      <p className="markdown-title">{markdownTitle}</p>
      <Markdown rehypePlugins={[rehypeHighlight]}>{markdown}</Markdown>
    </div>
  );
};

// MarkDownCodeSnippet.defaultProps . props is short for properties
MarkDownCodeSnippet.propTypes = {
  markdown: PropTypes.string.isRequired,
  markdownTitle: PropTypes.string.isRequired,
};

export default MarkDownCodeSnippet;
