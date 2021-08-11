import React, { useState, useEffect } from "react";
import api from "../../coffeeAPI";
import { Coffee } from "../../coffeeAPI";
import { CoffeeList } from "./CoffeeList";

interface Props {}

export const CoffeeListContainer = (props: Props) => {
  const [coffees, setCoffees] = useState<Coffee[]>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [errors, setErrors] = useState<any>(null);

  const fetchCoffees = async (): Promise<void> => {
    console.log("here");

    setIsLoading(true);

    try {
      const response = await api.getCoffeeList();

      setCoffees(response.data);
    } catch (err) {
      setErrors(err);

      throw err;
    } finally {
      setIsLoading(false);
    }
  };

  const handleAddNew = async (
    title: string,
    price: string,
    picture: File
  ): Promise<void> => {
    const formData = new FormData();

    formData.append("picture", picture);
    formData.append("title", title);
    formData.append("price", String(price));

    try {
      await api.addNewCoffee(formData);

      window.alert("coffee added successfully");
      fetchCoffees();
    } catch (error) {
      window.alert("coffee could not be created");

      throw error;
    }
  };

  const handleDelete = async (id: string): Promise<void> => {
    try {
      await api.deleteCoffee(id);

      window.alert("coffee deleted successfully");
      fetchCoffees();
    } catch (error) {
      window.alert("coffee could not be deleted");

      throw error;
    }
  };

  useEffect(() => {
    fetchCoffees();
  }, []);

  if (isLoading && !coffees) {
    return <div>Loading coffees</div>;
  }

  return (
    <CoffeeList
      data={coffees}
      onAddNew={handleAddNew}
      onDelete={handleDelete}
    />
  );
};
