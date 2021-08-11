import React, { useState } from "react";
import "./_coffeeList.scss";
import { Coffee } from "../../coffeeAPI";
import { AddCoffee } from "./AddCoffee";
import { Card } from "../Card/Card";

interface Props {
  data: Coffee[];
  onAddNew: (title: string, price: string, picture: File) => void;
  onDelete: (id: string) => void;
}

export const CoffeeList = (props: Props) => {
  const [isPopupOpen, setIsPopupOpen] = useState(false);

  const renderedCoffees = props.data.map((coffee) => (
    <Card
      imgSrc={"https://localhost:5001/" + coffee.imagePath}
      label={coffee.title}
      subLabel={String(coffee.price)}
      onSubmitClick={() => props.onDelete(coffee.id)}
    />
  ));

  const handleAddNew = (title: string, price: string, picture: File): void => {
    props.onAddNew(title, price, picture);

    setIsPopupOpen(false);
  };

  return (
    <section className="CoffeeList">
      <section>
        <h2>Coffee Billboard</h2>
        <div className="CoffeeList__items">{renderedCoffees}</div>
      </section>
      <button
        className="CoffeeList__add-new-button"
        onClick={() => setIsPopupOpen(!isPopupOpen)}
      >
        Add new
      </button>
      {isPopupOpen && (
        <div className="CoffeeList__add-new-popup">
          <AddCoffee onSubmitClick={handleAddNew} />
        </div>
      )}
    </section>
  );
};
