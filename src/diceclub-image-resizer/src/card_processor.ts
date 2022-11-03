import * as Scry from 'scryfall-sdk';


const processCard = async (name: string) => {
  try {
    //name = name.replace('\n', '');
    name = name.replace(/[^a-zA-Z -]/gm, '');
    name = name.trimLeft().trimEnd();
    //name = name.replace(/^[a-z\u0590-\u05fe]+$/i, '');
    //name = name.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
    //name = name.replace(/ /g, ' ');
    const card = await Scry.Cards.byName(name, true);
    // if (cards.length > 0) {
    //   const card = cards[0];
    //console.log(`Found card: ${card.name} - ${card.color_identity}`);
    // } else {
    //   console.log(`Can't find ${name}`);
    // }
    return card;
  } catch (ex ) {
    console.log(`Can't find card: ${name}`);
    return null;
  }
};

export { processCard };
