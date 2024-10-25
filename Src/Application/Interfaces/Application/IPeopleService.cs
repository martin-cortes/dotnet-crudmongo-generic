﻿using Core.Entities;

namespace Application.Interfaces.Application
{
    public interface IPeopleService
    {
        Task<string> CreatePeopleAsync(People people);

        Task<People> GetByIdAsync(string id);
    }
}