import { CreditCardInfoListModel } from '../models/CreditCardInfoListModel';
import { CreditCardInfoModel } from '../models/CreditCardInfoModel';

let cachedCreditCards: CreditCardInfoListModel;

export function setCreditCardInfos(data: CreditCardInfoModel[]) {
  cachedCreditCards = {
    creditCards: data,
    timestamp: new Date(),
  };
}

export function getCreditCardInfos(): CreditCardInfoListModel {
  return cachedCreditCards;
}
