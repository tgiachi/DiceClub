import React from "react";
import { Card, SemanticWIDTHS } from "semantic-ui-react";
import { MtgCardDto } from "../../../schemas/dice-club";
import { CardItem } from "../card.item";


export const CardResultGrid = ({cards, itemsPerRow, showDetails = true} : {cards: MtgCardDto[], itemsPerRow: SemanticWIDTHS, showDetails? :boolean}) => {
  return (
    <Card.Group itemsPerRow={itemsPerRow}>
    {cards.map((c) => {
      return <CardItem showDetails={showDetails} key={c.id} card={c} />;
    })}
  </Card.Group>
  )
}