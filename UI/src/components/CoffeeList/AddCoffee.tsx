import React, {
  ChangeEvent,
  ChangeEventHandler,
  MouseEvent,
  MouseEventHandler,
  useState,
} from "react";

interface Props {
  onSubmitClick: (title: string, price: string, picture: File) => void;
}

export const AddCoffee = (props: Props) => {
  const [title, setTitle] = useState<string>("");
  const [price, setPrice] = useState<string>("");
  const [picture, setPicture] = useState<File>();

  const handleSubmitClick: MouseEventHandler<HTMLButtonElement> = (
    event: MouseEvent
  ) => {
    event.preventDefault();
    props.onSubmitClick(title, price, picture);

    setTitle("");
    setPrice("");
  };

  const handlePriceChange: ChangeEventHandler<HTMLInputElement> = (
    event: ChangeEvent<HTMLInputElement>
  ) => {
    const enteredPrice = event.target.value;
    console.log(enteredPrice);
    setPrice(enteredPrice);
  };

  return (
    <form className="AddCoffee">
      <h3 className="AddCoffee__heading">Add new coffee</h3>
      <label htmlFor="coffeeTitle">Title</label>
      <input
        className="AddCoffee__text-field"
        type="text"
        id="coffeeTitle"
        name="coffeeTitle"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
      />
      <label htmlFor="coffeePrice">Price</label>
      <input
        type="number"
        min="1"
        step="any"
        className="AddCoffee__text-field"
        id="coffeePrice"
        name="coffeePrice"
        value={price}
        onChange={handlePriceChange}
      />
      <label htmlFor="coffeePicture">Picture</label>
      <input
        type="file"
        className="AddCoffee__file-field"
        id="coffeePicture"
        name="coffeePicture"
        onChange={(e) => setPicture(e.target.files[0])}
      />
      <button
        type="button"
        className="AddCoffee__submit-button"
        onClick={handleSubmitClick}
      >
        Submit
      </button>
    </form>
  );
};
