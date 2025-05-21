import { CreditCardInfoListModel } from '../models/CreditCardInfoListModel';
import { CreditCardInfoModel } from '../models/CreditCardInfoModel';

let cachedCreditCards: CreditCardInfoListModel = { creditCards: [], timestamp: new Date() };

export const setCreditCardInfos = (data: CreditCardInfoModel[]) => {
  cachedCreditCards = {
    creditCards: data,
    timestamp: new Date(),
  };
};

export const getCreditCardInfos = (): CreditCardInfoListModel => {
  return cachedCreditCards;
};
