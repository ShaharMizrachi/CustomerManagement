import React from "react";

export const getLocalVersion = () => {
  const storedData = localStorage.getItem("data");

  if (storedData) {
    const data = JSON.parse(storedData);
    return parseFloat(data.version);
  }
  return null;
};
