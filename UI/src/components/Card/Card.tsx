import React from "react";
import "./_card.scss";
// import TrashIcon from "../../assets/trash-fill.svg";

interface Props {
  imgSrc: string;
  label: string;
  subLabel: string;
  onSubmitClick: () => void;
}

export const Card = (props: Props) => {
  return (
    <figure className="Card">
      <img className="Card__image" src={props.imgSrc} />
      <div className="Card__details">
        <strong className="Card__details__label">{props.label}</strong>
        <span className="Card__details__sub-label">
          {props.subLabel}&nbsp;$
        </span>
      </div>
      <button
        className="Card__details__remove-button"
        type="button"
        onClick={() => props.onSubmitClick()}
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="16"
          height="16"
          fill="currentColor"
          className="bi bi-trash-fill"
          viewBox="0 0 16 16"
        >
          <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
        </svg>
      </button>
    </figure>
  );
};
