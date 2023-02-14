﻿using System.Security.Claims;
using TodoList.DTO.User;
using TodoList.Models.User;

namespace UnitTests.TestDataCollections.ServicesTests.UserServiceTests;

internal class UserServiceTestsDataInit
{
    public static IEnumerable<object[]> UserAccessDeniedCheckCollectionInit()
    {
        const int MAX_COUNT = 2;

        var userAccessDeniedCheckCollection = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, i.ToString())
            };

            userAccessDeniedCheckCollection.Add(new object[]
            {
                new UserAccessDeniedCheck()
                {
                    UserId = i,
                    UserClaims = claims
                }
            });
        }

        return userAccessDeniedCheckCollection;
    }

    public static IEnumerable<object[]> UserAccessDeniedCheckSuitCollectionInit()
    {
        const int MAX_COUNT = 2;

        var userAccessDeniecCheckSuitCollection = new List<object[]>();

        for (int i = 0; i < MAX_COUNT; i++)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, i.ToString())
            };

            userAccessDeniecCheckSuitCollection.Add(new object[]
            {
                new UserAccessDeniedCheck()
                {
                    UserId = i,
                    UserClaims = claims
                },
                new User()
                {
                    Id = i,
                    NickName = $"nickname-user-with-id-{i}",
                    Login = $"login-user-with-id-{i}",
                    Password = $"password-user-with-id-{i}"
                }
            });
        }

        return userAccessDeniecCheckSuitCollection;
    }
}