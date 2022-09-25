import React from "react";

import { observer } from "mobx-react";

import { useStore } from "../../stores/store.context";
import { MtgCardSetDto } from "../../schemas/dice-club";
import { Select } from "semantic-ui-react";

export const SetSelectComponent = observer(() => {
  const [sets, setSelect] = React.useState([] as MtgCardSetDto[]);
  const { rootStore } = useStore();

  React.useEffect(() => {
    setSelect(rootStore.cardsStore.sets!);
  }, [rootStore.cardsStore.sets]);

  return (
    <Select
      fluid
      multiple
      search
      clearable
      selection
      options={sets.map((s) => {
        return {
          key: s.code!,
          value: s.code!,
          text: s.description!,
          image: {
            avatar: true,
            src: s.image,
            style: { width: "20px", height: "20px" },
          },
        };
      })}
    ></Select>
  );
});
