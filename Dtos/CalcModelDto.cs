using System;
using Amazon.DynamoDBv2.DataModel;
using NewSampleAPI.Domain.Enum;

namespace NewSampleAPI.Application.Dtos
{
    [DynamoDBTable("DemoCal")]
    public class CalcModelDto
    {
        [DynamoDBHashKey]
        public Guid Id { get; init; } = Guid.NewGuid();

        [DynamoDBProperty]
        public int FirstOperand { get; init; }

        [DynamoDBProperty]
        public int SecondOperand { get; init; }

        [DynamoDBProperty]
        public CalcEnum Operators { get; set; }

        [DynamoDBProperty]
        public int Result { get; set; }

        [DynamoDBProperty]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }

}
