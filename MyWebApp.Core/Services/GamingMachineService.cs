using MyWebApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MyWebApp.Core.Entities;
using MyWebApp.Core.Entities.Result;
using MyWebApp.Core.Specifications;
using System.Linq;

namespace MyWebApp.Core.Services
{
    public class GamingMachineService : IGamingMachine
    {        
        private readonly IRepository<GamingMachine> _repository;

        public GamingMachineService(IRepository<GamingMachine> repository)
        {
            _repository = repository;
        }

        public IEnumerable<GamingMachine> Get(out int totalRecords, int page = 0, int skip = 10, string filter = "")
        {
            var totalSkip = page * skip;
            IEnumerable<GamingMachine> result;

            if (String.IsNullOrWhiteSpace(filter))            
                result = _repository.ListAll();
            else 
                result = _repository.List(new GamingMachineFilterSpecification(filter.Trim()));

            totalRecords = result.Count();
            return result.Skip(totalSkip).Take(skip);
        }

        public GamingMachine Get(long gamingSerialNumber)
        {
            return _repository.List(new GamingMachineFilterSpecification(gamingSerialNumber)).FirstOrDefault();
        }

        public Result CreateGamingMachine(GamingMachine gamingMachine)
        {
            try
            {
                var errors = Validate(gamingMachine);

                if (errors.Count() > 0)
                    return Result.Failed(errors.ToArray());

                _repository.Add(gamingMachine);
                return Result.Success;
            }
            catch (Exception ex)
            {
                return Result.Failed(new ResultError[]
                {
                    new ResultError
                    {
                        Code = GamingMachineServiceErrors.ADD_ERROR_CODE,
                        Message = ex.Message
                    }
                });
            }
        }

        public Result UpdateGamingMachine(GamingMachine gamingMachine)
        {
            try
            {
                var errors = Validate(gamingMachine, false);

                if (errors.Count() > 0)
                    return Result.Failed(errors.ToArray());

                _repository.Update(gamingMachine);
                return Result.Success;
            }
            catch (Exception ex)
            {
                return Result.Failed(new ResultError[]
                {
                    new ResultError
                    {
                        Code = GamingMachineServiceErrors.UPDATE_ERROR_CODE,
                        Message = ex.Message
                    }
                });
            }
        }

        public Result DeleteGamingMachine(GamingMachine gamingMachine)
        {
            try
            {
                _repository.Delete(gamingMachine);
                return Result.Success;
            }
            catch (Exception ex)
            {
                return Result.Failed(new ResultError[]
                {
                    new ResultError
                    {
                        Code = GamingMachineServiceErrors.DELETE_ERROR_CODE,
                        Message = ex.Message
                    }
                });
            }
        }

        private ResultError[] Validate(GamingMachine gamingMachine, bool checkUnique = true)
        {
            var errors = new List<ResultError>();

            if (checkUnique)
            {
                if (!IsGamingSerialNumberUnique(gamingMachine.GamingSerialNumber))
                    errors.Add(new ResultError
                    {
                        Code = GamingMachineServiceErrors.SERIALNUMBER_ERROR_CODE,
                        Message = GamingMachineServiceErrors.SERIALNUMBER_ERROR_MESSAGE
                    });
            }

            if (String.IsNullOrWhiteSpace(gamingMachine.GameName))
                errors.Add(new ResultError
                {
                    Code = GamingMachineServiceErrors.GAMENAME_ERROR_CODE,
                    Message = GamingMachineServiceErrors.GAMENAME_ERROR_MESSAGE
                });

            return errors.ToArray();
        }

        private bool IsGamingSerialNumberUnique(long gamingSerialNumber)
        {
            if (Get(gamingSerialNumber) == null)
                return true;

            return false;
        }
    }
}
