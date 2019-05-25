﻿namespace RobloxFiles.BinaryFormat.Chunks
{
    public class INST
    {
        public readonly int TypeIndex;
        public readonly string TypeName;
        public readonly bool IsService;
        public readonly int NumInstances;
        public readonly int[] InstanceIds;

        public override string ToString()
        {
            return TypeName;
        }

        public INST(BinaryRobloxFileChunk chunk)
        {
            using (BinaryRobloxFileReader reader = chunk.GetDataReader())
            {
                TypeIndex = reader.ReadInt32();
                TypeName = reader.ReadString();
                IsService = reader.ReadBoolean();

                NumInstances = reader.ReadInt32();
                InstanceIds = reader.ReadInstanceIds(NumInstances);
            }
        }

        public void Allocate(BinaryRobloxFile file)
        {
            foreach (int instId in InstanceIds)
            {
                Instance inst = new Instance() { ClassName = TypeName };
                file.Instances[instId] = inst;
            }

            file.Types[TypeIndex] = this;
        }
    }
}