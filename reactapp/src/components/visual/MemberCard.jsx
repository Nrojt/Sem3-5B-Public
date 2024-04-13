// ProfileCard.jsx
import { Card, Col, Image, ListGroup } from "react-bootstrap";
import PropTypes from "prop-types";
import React from "react";

const MemberCard = ({ imageUrl, name, subtitle }) => {
  return (
    <Col>
      <Card data-testid="membercard" border="0" className="shadow-none">
        <Card.Body className="d-flex align-items-center p-0">
          <Image
            className="rounded-circle flex-shrink-0 me-3 fit-cover"
            src={imageUrl}
            width={130}
            height={130}
          />
          <div>
            <Card.Title className="fw-bold text-primary mb-0">
              {name}
            </Card.Title>
            <Card.Subtitle className="text-muted mb-1">
              {subtitle}
            </Card.Subtitle>
            <ListGroup horizontal className="fs-6 text-muted w-100 mb-0">
              <ListGroup.Item className="text-center">
                <div className="d-flex flex-column align-items-center">
                  {/* Facebook SVG */}
                </div>
              </ListGroup.Item>
              <ListGroup.Item className="text-center">
                <div className="d-flex flex-column align-items-center">
                  {/* Instagram SVG */}
                </div>
              </ListGroup.Item>
            </ListGroup>
          </div>
        </Card.Body>
      </Card>
    </Col>
  );
};

// ProfileCard.defaultProps . props is short for properties
MemberCard.propTypes = {
  imageUrl: PropTypes.string.isRequired,
  name: PropTypes.string.isRequired,
  subtitle: PropTypes.string.isRequired,
};

export default MemberCard;
