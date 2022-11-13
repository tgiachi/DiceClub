import axios from 'axios';
import { Card } from 'scryfall-sdk';

export interface ILoginResult {
  accessToken: string;
  refreshToken: string;
  accessTokenExpire: string;
}

const sendToWebService = async (baseUrl: string, card: Card, token: string) => {
  await axios.post(`${baseUrl}/api/v1/cards/add/${card.id}`, null, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  } );
};

const loginToWebService = async (baseUrl: string, email: string, password: string) => {
  const response = await axios.post(`${baseUrl}/api/v1/login/auth`, {
    email,
    password,
  });
  return response.data.result as ILoginResult;
};


export { sendToWebService, loginToWebService };