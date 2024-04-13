import "../../../styles/AllResearch.css";
import { useTranslation } from "react-i18next";
import { getAllResearches } from "@/utils/api/pages/researchcontroller.js";
import { useEffect, useState } from "react";
import { Row, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

// Page for all researches
function AllResearch() {
  const [researches, setResearches] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Fetch disabilityExpert from database
    const fetchData = async () => {
      try {
        const response = await getAllResearches();
        console.log(response.data);

        // Update state with fetched data
        setResearches(response.data);
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []); // Empty dependency array ensures that the effect runs only once

  // loading in translation
  const [t] = useTranslation("allResearch_page");

  const navigate = useNavigate();

  // method for handling the read more button
  const handleReadMore = (research) => {
    console.log("read more button clicked ", research);

    navigate("/research?researchId=" + research.researchId, {
      state: {
        researchData: research,
      },
    });
  };

  return (
    <div className="allResearch">
      <div className="research-inhoud mb-3">
        <h1 className="text-center onderzoeken-titel">
          {t("allResearch_title")}
        </h1>
        <div>
          {loading ? (
            <h2 className="text-center mb-3">{t("loading")}</h2>
          ) : (
            <Row>
              <div className="research">
                {researches.map((research) => (
                  <div key={research.researchId} className="research_item">
                    <h2 className="research_item_title">{research.title} </h2>
                    <p className="research_item_description">
                      {research.description}{" "}
                    </p>
                    <p>
                      <strong>{t("researchType")}</strong>{" "}
                      {research.researchType}
                    </p>
                    <p>
                      <strong>{t("reward")}</strong> {research.reward}
                    </p>
                    <Button
                      variant="primary"
                      className="leesMeerKnop"
                      onClick={() => handleReadMore(research)}
                    >
                      {t("readMore")}
                    </Button>
                  </div>
                ))}
              </div>
            </Row>
          )}
        </div>
      </div>
    </div>
  );
}

export default AllResearch;
