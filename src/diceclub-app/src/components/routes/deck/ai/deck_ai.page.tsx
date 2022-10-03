import React from "react";

import { observer } from "mobx-react";
import { useStore } from "../../../../stores/store.context";
import { Segment } from "semantic-ui-react";
import { DeckAiSearchComponent } from "../../../deck/ai/deck.ai.search";

export const DeckAiPage = observer(() => {
  const { rootStore } = useStore();

  return <>
  <Segment>
    <DeckAiSearchComponent />
  </Segment>
  </>
})