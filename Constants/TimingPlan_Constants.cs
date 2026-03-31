using System;
using System.Linq;

namespace RoadSafe.TimingPlanModule.Constants
{
	public static class TimingPlanStatus
	{
		public const string Draft = "Draft";
		public const string PendingApproval = "PendingApproval";
		public const string Approved = "Approved";
		public const string Active = "Active";
		public const string Archived = "Archived";

		// A quick array of all valid statuses
		public static readonly string[] AllStatuses =
		{
			Draft,
			PendingApproval,
			Approved,
			Active,
			Archived
		};

		// Helper method to validate incoming API requests
		public static bool IsValid(string status)
		{
			return AllStatuses.Contains(status, StringComparer.OrdinalIgnoreCase);
		}
	}
}