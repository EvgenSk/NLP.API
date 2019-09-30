using Orleans.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLP.API.OrleansHostingExtensions
{
	public interface IStanfordNLPGrainServiceClient : IGrainServiceClient<IStanfordNLPGrainService>, IStanfordNLPGrainService
	{
	}
}
