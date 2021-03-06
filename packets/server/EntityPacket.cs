using System;

namespace Packets
{
    public class EntityPacket : Packet {
        public override short id { get{ return 8;} }
        public override string name { get{ return "Entity";} }

        public static DataType[] schema = {
            new DataInt(),
            new DataInt(),
            new DataUShort(),
            new DataByte(),
            new DataByte(),
            new DataString(),
            new DataFixedString(32)
        };

        public int x;
        public int y;
        public int facing;
        public ushort type;
        public bool interactable;
        public string sprite;
        public string uuid;

        public EntityPacket(int pos_x, int pos_y, ushort entity_type, int entity_facing, bool entity_interactable, string entity_sprite, Guid entity_uuid )
        {
            x = pos_x;
            y = pos_y;
            facing = entity_facing;
            type = entity_type;
            interactable = entity_interactable;
            sprite = entity_sprite;
            uuid = entity_uuid.ToString("N");
        }

        public EntityPacket(LegendSharp.Entity entity)
        {
            x = entity.pos.x;
            y = entity.pos.y;
            facing = (int)entity.facing;
            sprite = entity.sprite;
            uuid = entity.uuid.ToString("N");
            if (entity is LegendSharp.Player)
            {
                type = 2;
                interactable = false;
            }
            else if (entity is LegendSharp.NPC)
            {
                type = 1;
                interactable = true;
            }
            else
            {
                type = 0;
                interactable = false;
            }
        }

        public EntityPacket(byte[] received_data)
        {
            var decoded = Packets.decodeData(schema, received_data);
            x = (int) decoded[0];
            y = (int) decoded[1];
            type = (ushort) decoded[2];
            facing = (int) decoded[3];
            interactable = ((int) decoded[4]) == 1;
            sprite = (string) decoded[5];
            uuid = (string) decoded[6];
        }

        public override byte[] encode()
        {
            var output = Packets.encodeData(schema, new object[] {x, y, type, facing, interactable ? 1 : 0, sprite, uuid});
            return output;
        }
    }

}