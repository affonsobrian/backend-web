using Backend_Web.Models;
using Backend_Web.Utils;
using System;
using System.Collections.Generic;

namespace Backend_Web.Services
{
    public class ActionService : BaseService<Action>
    {
        public BaseResponse<bool> Borrow(int personId, List<int> properties, DateTime date, string document = null)
        {
            BaseResponse<Person> personResponse = new PersonService().Find(personId);
            return Borrow(personResponse, properties, date, document);
        }

        public BaseResponse<bool> Borrow(string email, List<int> properties, DateTime date, string document = null)
        {
            BaseResponse<Person> personResponse = new PersonService().FindByEmail(email);
            return Borrow(personResponse, properties, date, document);
        }

        private BaseResponse<bool> Borrow(BaseResponse<Person> personResponse, List<int> properties, DateTime date, string document)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            PropertyService propertyService = new PropertyService();
            List<Transaction> transactions = new List<Transaction>();

            if (personResponse.Status != Status.OK)
            {
                response.Status = personResponse.Status;
                response.Message = personResponse.Message;
                response.Content = false;
            }
            else
            {
                foreach (int propertyId in properties)
                {
                    BaseResponse<Property> propertyResponse = propertyService.Find(propertyId);
                    if (propertyResponse.Status != Status.OK)
                    {
                        response.Status = propertyResponse.Status;
                        response.Message = propertyResponse.Message;
                        response.Content = false;
                        return response;
                    }
                    else if (propertyResponse.Content.Status == "Emprestado")
                    {
                        response.Status = Status.ERROR;
                        response.Message = Resources.ErrorMessages.alreadyBorrowed;
                        response.Content = false;
                        return response;
                    }

                    Property property = propertyResponse.Content;
                    property.Person = personResponse.Content;
                    property.Status = "Emprestado";
                    propertyService.Edit(property);
                    Transaction transaction = new Transaction { Date = date, PropertyId = propertyId, Type = TransactionType.Loan, Document = document };
                    transactions.Add(transaction);
                }

                HistoryService historyService = new HistoryService();
                historyService.Insert(new History { Transactions = transactions, PersonId = personResponse.Content.Id });
                response.Status = Status.OK;
                response.Message = Resources.Commun.success;
                response.Content = true;
            }
            return response;
        }

        public BaseResponse<bool> Return(int personId, List<int> properties, DateTime date, string document = null)
        {
            BaseResponse<Person> personResponse = new PersonService().Find(personId);
            return Return(personResponse, properties, date, document);
        }

        public BaseResponse<bool> Return(string email, List<int> properties, DateTime date, string document = null)
        {
            BaseResponse<Person> personResponse = new PersonService().FindByEmail(email);
            return Return(personResponse, properties, date, document);
        }

        public BaseResponse<bool> Return(BaseResponse<Person> personResponse, List<int> properties, DateTime date, string document = null)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            PropertyService propertyService = new PropertyService();
            List<Transaction> transactions = new List<Transaction>();

            if (personResponse.Status != Status.OK)
            {
                response.Status = personResponse.Status;
                response.Message = personResponse.Message;
                response.Content = false;
            }
            else
            {
                if (personResponse.Status != Status.OK)
                {
                    response.Status = personResponse.Status;
                    response.Message = personResponse.Message;
                    response.Content = false;
                }
                else
                {
                    foreach (int propertyId in properties)
                    {
                        BaseResponse<Property> propertyResponse = propertyService.Find(propertyId);
                        if (propertyResponse.Status != Status.OK)
                        {
                            response.Status = propertyResponse.Status;
                            response.Message = propertyResponse.Message;
                            response.Content = false;
                            return response;
                        }
                        else if (propertyResponse.Content.Status == "Disponível")
                        {
                            response.Status = Status.ERROR;
                            response.Message = Resources.ErrorMessages.available;
                            response.Content = false;
                            return response;
                        }
                        else if (propertyResponse.Content.Person.Id != personResponse.Content.Id)
                        {
                            response.Status = Status.ERROR;
                            response.Message = Resources.ErrorMessages.personDontMatch;
                            response.Content = false;
                            return response;
                        }
                        Property property = propertyResponse.Content;
                        property.Person = null;
                        property.Status = "Disponível";
                        propertyService.Edit(property);
                        Transaction transaction = new Transaction { Date = date, PropertyId = propertyId, Type = TransactionType.Return };
                        transactions.Add(transaction);
                    }

                    HistoryService historyService = new HistoryService();
                    historyService.Insert(new History { Transactions = transactions, PersonId = personResponse.Content.Id });
                    response.Status = Status.OK;
                    response.Message = Resources.Commun.success;
                    response.Content = true;
                }
            }
            return response;
        }
    }
}