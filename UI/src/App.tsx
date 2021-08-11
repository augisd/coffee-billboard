import React from "react";
import api from "./coffeeAPI";
import { CoffeeListContainer } from "./components/CoffeeList/CoffeeListContainer";

export const App: React.FC = () => {
  return <CoffeeListContainer />;
};
