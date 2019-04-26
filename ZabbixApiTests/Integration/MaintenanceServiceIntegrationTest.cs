using System;
using System.Collections.Generic;
using Xunit;
using ZabbixApi.Entities;

namespace ZabbixApiTests.Integration
{
    public class MaintenanceServiceIntegrationTest : IntegrationTestBase
    {
        [Fact]
        public void MustGetAny()
        {
            var result = context.Maintenance.Get();
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateTest()
        {
            var result = context.Maintenance.Get();
            Assert.NotNull(result);

            var hostGroup1 = context.HostGroups.GetByName("Group01");
            Assert.Equal("Group01", hostGroup1.name);

            var host1 = context.Hosts.GetByName("zabbix-agent01");
            Assert.Equal("zabbix-agent01", host1.name);

            var group1 = new List<HostGroup>() { hostGroup1 };
            var entity = new Maintenance()
            {
                //{
                //    "jsonrpc": "2.0",
                //    "method": "maintenance.create",
                //    "params": {
                //        "name": "Sunday maintenance",
                name = "Sunday maintenance",
                //        "active_since": 1358844540,
                active_since = DateTimeOffset.FromUnixTimeSeconds(1358844540).DateTime,
                //        "active_till": 1390466940,
                active_till = DateTimeOffset.FromUnixTimeSeconds(1390466940).DateTime,
                //        "tags_evaltype": 0,
                //        "groupids": [
                //            "2"
                //        ],

                // Issue
                groups = group1,
                // Resolved with (Does not work)
                // Id = group1[0].Id,

                //        "timeperiods": [
                //            {
                //                "timeperiod_type": 3,
                //                "every": 1,
                //                "dayofweek": 64,
                //                "start_time": 64800,
                //                "period": 3600
                //            }
                //        ],
                timeperiods = new List<TimePeriod>() {
                    new TimePeriod() {
                        timeperiod_type = TimePeriod.TimePeriodType.Weekly,
                        every = TimePeriod.Every.FirstWeek,
                        dayofweek = 64,
                        start_time = 64800,
                        period = 3600
                    }
                },
                //        "tags": [
                //            {
                //                "tag": "service",
                //                "operator": "0",
                //                "value": "mysqld",
                //            },
                //            {
                //                "tag": "error",
                //                "operator": "2",
                //                "value": ""
                //            }
                //        ]
                //    },
                //    "auth": "038e1d7b1735c6a5436ee9eae095879e",
                //    "id": 1
                //}
                //maintenance_type = Maintenance.MaintenanceType.WithoutDataCollection,
                //hosts = new List<Host> { host1 }
            };

            var result2 = context.Maintenance.Create(entity);
            Console.WriteLine("Result : {0}", result2);
        }
    }
}
