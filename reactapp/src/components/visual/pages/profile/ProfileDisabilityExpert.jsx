import { useState, useEffect } from "react";

import { getProfilePageDisabilityExpert } from "../../../../utils/api/pages/profilepage";
import ProfileCard from "./ProfileCard";
import { useMutation } from "@tanstack/react-query";

import { Card } from "react-bootstrap";
import { useTranslation } from "react-i18next";

import PropTypes from "prop-types";

ProfileDisabilityExpert.propTypes = {
  loadingText: PropTypes.string.isRequired,
  disabilityExpert: PropTypes.object.isRequired,
  setDisabilityExpert: PropTypes.func.isRequired,
};

export default function ProfileDisabilityExpert({
  loadingText,
  disabilityExpert,
  setDisabilityExpert,
}) {
  const [t] = useTranslation("profile_page_disabilityexpert");

  const [user, setUser] = useState({
    firstName: "",
    lastName: "",
    birthYear: 0,
    email: "",
    postalCode: "",
    disabilities: "",
    guardian: "",
    typeBenadering: "",
  });

  const updateFirstName = (newFirstName) => {
    setDisabilityExpert((prevData) => ({
      ...prevData,
      firstName: newFirstName,
    }));
  };
  const updateLastName = (newLastName) => {
    setDisabilityExpert((prevData) => ({
      ...prevData,
      lastName: newLastName,
    }));
  };
  const updateBirthYear = (newBirthYear) => {
    setDisabilityExpert((prevData) => ({
      ...prevData,
      birthYear: newBirthYear,
    }));
  };
  const updateEmail = (newEmail) => {
    setDisabilityExpert((prevData) => ({
      ...prevData,
      email: newEmail,
    }));
  };
  const updatePostalCode = (newPostalCode) => {
    setDisabilityExpert((prevData) => ({
      ...prevData,
      postalCode: newPostalCode,
    }));
  };
  const updateDisabilities = (newDisabilities) => {
    setDisabilityExpert((prevData) => ({
      ...prevData,
      disabilities: newDisabilities,
    }));
  };
  const updateGuardian = (newGuardian) => {
    setDisabilityExpert((prevData) => ({
      ...prevData,
      guardian: newGuardian,
    }));
  };
  const updateTypebenadering = (newTypeBenadering) => {
    setDisabilityExpert((prevData) => ({
      ...prevData,
      typeBenadering: newTypeBenadering,
    }));
  };

  const { mutate, isPending, error, isSuccess } = useMutation({
    mutationFn: async () => {
      const response = getProfilePageDisabilityExpert();
      return response;
    },

    onSuccess: (response) => {
      if (response && response.status === 200) {
        response.data.userType = "DisabilityExpert";
        console.log("got disabilityExpert from database", response.data);
        if (response.data) {
          setDisabilityExpert(response.data);
        } else {
          throw new Error(
            "Getting disabilityExpert from database, response has no data",
          );
        }
      } else {
        throw new Error(
          "Getting disabilityExpert from database, response not 200",
        );
      }
    },

    onError: () => {
      console.log("Encountered an error while trying to log in");
      return error;
    },
  });

  useEffect(() => {
    // Fetch disabilityExpert from database
    mutate();
  }, []); // Empty dependency array ensures that the effect runs only once

  if (isPending) {
    return <div>{loadingText}</div>;
  }

  if (isSuccess) {
    return (
      <Card.Body>
        <ProfileCard
          label={t("first_name")}
          originalValue={disabilityExpert.firstName}
          setValueParent={updateFirstName}
        />
        <ProfileCard
          label={t("last_name")}
          originalValue={disabilityExpert.lastName}
          setValueParent={updateLastName}
        />
        <ProfileCard
          label={t("Geboortejaar")}
          originalValue={String(disabilityExpert.birthYear)}
          setValueParent={updateBirthYear}
        />
        <ProfileCard
          label={t("Email")}
          originalValue={disabilityExpert.email}
          setValueParent={updateEmail}
        />
        <ProfileCard
          label={t("Adres")}
          originalValue={disabilityExpert.postalCode}
          setValueParent={updatePostalCode}
        />
        <ProfileCard
          label={t("Beperking")}
          originalValue={disabilityExpert.disabilities}
          setValueParent={updateDisabilities}
        />
        <ProfileCard
          label={t("guardian")}
          originalValue={disabilityExpert.guardian}
          setValueParent={updateGuardian}
        />
        <ProfileCard
          label={t("Benadering")}
          originalValue={disabilityExpert.typeBenadering}
          setValueParent={updateTypebenadering}
        />
      </Card.Body>
    );
  }

  return null;
}
