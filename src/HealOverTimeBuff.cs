using System;
using BuffStuff;
using ProtoBuf;
using Vintagestory.API.Common;

namespace HotPoultice {
  [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
  public class HealOverTimeBuff : Buff {
    [ProtoIgnore]
    private static int DURATION_TICKS = 4 * 8;
    public double hpPerTick;
    public void init(double totalHealthChange) {
      hpPerTick = totalHealthChange / DURATION_TICKS;
      SetExpiryInTicks(DURATION_TICKS);
    }
    public override void OnTick() {
      Entity.ReceiveDamage(new DamageSource {
        Source = EnumDamageSource.Internal,
        Type = hpPerTick < 0 ? EnumDamageType.Poison : EnumDamageType.Heal
      }, (float)hpPerTick);
    }
  }

  // public class HealOverTimeBuff : Buff {
  //   [ProtoIgnore]
  //   private static float HP_PER_TICK = 1f / 12f;
  //   public bool isPoison;
  //   public void init(double hpRemaining) {
  //     isPoison = hpRemaining < 0;
  //     SetExpiryInTicks((int)Math.Round(Math.Abs(hpRemaining) / HP_PER_TICK));
  //   }
  //   public override void OnTick() {
  //     Entity.ReceiveDamage(new DamageSource {
  //       Source = EnumDamageSource.Internal,
  //       Type = isPoison ? EnumDamageType.Poison : EnumDamageType.Heal
  //     }, HP_PER_TICK);
  //   }
  // }
}