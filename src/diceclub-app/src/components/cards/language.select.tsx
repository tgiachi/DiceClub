import React, { useEffect } from "react";

import { observer } from "mobx-react";

import { useStore } from "../../stores/store.context";

import { Select } from "semantic-ui-react";
import { MtgCardLanguageDto } from "../../schemas/dice-club";

export const LanguageSelectComponent = observer(
  ({ multiselect = false }: { multiselect: boolean }) => {
    const [languages, setSelect] = React.useState([] as MtgCardLanguageDto[]);
    const { rootStore } = useStore();
    useEffect(() => {
      setSelect(rootStore.cardsStore.languages!);
    }, [rootStore.cardsStore.languages]);

    return (
      <Select
        fluid
        multiple={multiselect}
        search
        clearable
        selection
        options={languages.map((s) => {
          return {
            key: s.code!,
            value: s.code!,
            text: s.name,
          };
        })}
      ></Select>
    );
  }
);
