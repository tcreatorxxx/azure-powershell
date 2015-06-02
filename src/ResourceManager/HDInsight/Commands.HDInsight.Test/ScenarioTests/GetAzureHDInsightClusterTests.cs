﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.Azure.Commands.HDInsight.Models;
using Microsoft.Azure.Management.HDInsight.Models;
using Microsoft.WindowsAzure.Commands.ScenarioTest;
using Moq;
using Xunit;

namespace Microsoft.Azure.Commands.HDInsight.Test
{
    public class GetAzureHDInsightClusterTests : HDInsightTestBase
    {
        private GetAzureHDInsightCommand cmdlet;
        private const string ClusterName = "hdicluster";

        public GetAzureHDInsightClusterTests()
        {
            base.SetupTest();

            cmdlet = new GetAzureHDInsightCommand
            {
                CommandRuntime = commandRuntimeMock.Object,
                HDInsightManagementClient = hdinsightManagementClient.Object,
                ClusterName = ClusterName,
                ResourceGroupName = ResourceGroupName
            };
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void CanGetHDInsightCluster()
        {
            var cluster = new Cluster
            {
                Id = "id",
                Name = ClusterName,
                Location = Location,
                Properties = new ClusterGetProperties
                {
                    ClusterVersion = "3.1",
                    ClusterState = "Running",
                    ClusterDefinition = new ClusterDefinition
                    {
                        ClusterType = HDInsightClusterType.Hadoop
                    },
                    QuotaInfo = new QuotaInfo
                    {
                        CoresUsed = 24
                    },
                    OperatingSystemType = OSType.Windows
                }
            };

            var getresponse = new ClusterGetResponse { Cluster = cluster };
            hdinsightManagementClient.Setup(c => c.Get(ResourceGroupName, ClusterName))
                .Returns(getresponse)
                .Verifiable(); 
            
            hdinsightManagementClient.Setup(c => c.GetCluster(It.IsAny<string>(), It.IsAny<string>()))
                .CallBase()
                .Verifiable();
            
            cmdlet.ExecuteCmdlet();

            commandRuntimeMock.VerifyAll();
            commandRuntimeMock.Verify(f => f.WriteObject(It.IsAny<List<AzureHDInsightCluster>>(), true), Times.Once);
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void CanListHDInsightClustersInRG()
        {
            var cluster1 = new Cluster
            {
                Id = "id",
                Name = ClusterName + "1",
                Location = Location,
                Properties = new ClusterGetProperties
                {
                    ClusterVersion = "3.1",
                    ClusterState = "Running",
                    ClusterDefinition = new ClusterDefinition
                    {
                        ClusterType = HDInsightClusterType.Hadoop
                    },
                    QuotaInfo = new QuotaInfo
                    {
                        CoresUsed = 24
                    },
                    OperatingSystemType = OSType.Windows
                }
            };

            var cluster2 = new Cluster
            {
                Id = "id",
                Name = ClusterName + "2",
                Location = Location,
                Properties = new ClusterGetProperties
                {
                    ClusterVersion = "3.1",
                    ClusterState = "Running",
                    ClusterDefinition = new ClusterDefinition
                    {
                        ClusterType = HDInsightClusterType.Hadoop
                    },
                    QuotaInfo = new QuotaInfo
                    {
                        CoresUsed = 24
                    },
                    OperatingSystemType = OSType.Windows
                }
            };

            var listresponse = new ClusterListResponse {Clusters = new[] {cluster1, cluster2}};
            hdinsightManagementClient.Setup(c => c.ListClusters(ResourceGroupName))
                .Returns(listresponse)
                .Verifiable();

            hdinsightManagementClient.Setup(c => c.GetCluster(It.IsAny<string>(), It.IsAny<string>()))
                .CallBase()
                .Verifiable();

            cmdlet.ExecuteCmdlet();

            commandRuntimeMock.VerifyAll();
            commandRuntimeMock.Verify(f => f.WriteObject(It.IsAny<List<AzureHDInsightCluster>>(), true), Times.Once);
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void CanListHDInsightClusters()
        {
            var cluster1 = new Cluster
            {
                Id = "id",
                Name = ClusterName + "1",
                Location = Location,
                Properties = new ClusterGetProperties
                {
                    ClusterVersion = "3.1",
                    ClusterState = "Running",
                    ClusterDefinition = new ClusterDefinition
                    {
                        ClusterType = HDInsightClusterType.Hadoop
                    },
                    QuotaInfo = new QuotaInfo
                    {
                        CoresUsed = 24
                    },
                    OperatingSystemType = OSType.Windows
                }
            };

            var cluster2 = new Cluster
            {
                Id = "id",
                Name = ClusterName + "2",
                Location = Location,
                Properties = new ClusterGetProperties
                {
                    ClusterVersion = "3.1",
                    ClusterState = "Running",
                    ClusterDefinition = new ClusterDefinition
                    {
                        ClusterType = HDInsightClusterType.Hadoop
                    },
                    QuotaInfo = new QuotaInfo
                    {
                        CoresUsed = 24
                    },
                    OperatingSystemType = OSType.Windows
                }
            };

            var listresponse = new ClusterListResponse { Clusters = new[] { cluster1, cluster2 } };
            hdinsightManagementClient.Setup(c => c.ListClusters(ResourceGroupName))
                .Returns(listresponse)
                .Verifiable();

            hdinsightManagementClient.Setup(c => c.GetCluster(It.IsAny<string>(), It.IsAny<string>()))
                .CallBase()
                .Verifiable();

            cmdlet.ExecuteCmdlet();

            commandRuntimeMock.VerifyAll();
            commandRuntimeMock.Verify(f => f.WriteObject(It.IsAny<List<AzureHDInsightCluster>>(), true), Times.Once);
        }
    }
}
