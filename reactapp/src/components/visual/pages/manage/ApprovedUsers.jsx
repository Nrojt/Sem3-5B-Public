import React from "react";

function ApprovedUsers({ fullName, userName, accountDetails }) {
  return (
    <li
      style={{
        color: "transparent",
        marginLeft: "-30px",
        marginTop: 6,
        marginBottom: 6,
      }}
    >
      <div className="card manage-card" style={{ borderRadius: 20 }}>
        <div
          className="card-body"
          style={{
            background: "var(--bs-tertiary-bg)",
            borderRadius: 20,
          }}
        >
          <h4 className="card-title">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="1em"
              height="1em"
              fill="currentColor"
              viewBox="0 0 16 16"
              className="bi bi-person-circle"
              style={{ fontSize: 79 }}
            >
              <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0z" />
              <path
                fillRule="evenodd"
                d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8zm8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1z"
              />
            </svg>
            &nbsp; {fullName}
          </h4>
          <h6 className="text-muted card-subtitle mb-2">{userName}</h6>
          <p className="card-text">{accountDetails}</p>
        </div>
      </div>
    </li>
  );
}

export default ApprovedUsers;
