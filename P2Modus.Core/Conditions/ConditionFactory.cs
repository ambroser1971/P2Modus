using System;
using System.Collections.Generic;

namespace P2Modus.Core.Conditions
{
    public class ConditionFactory : IConditionFactory
    {
        public ConditionFactory()
        {}

        public virtual IEnumerable<int> GetConditionIdsInGroup(ConditionGroup group)
        {
            switch(group)
            {
                case ConditionGroup.None:
                    return GetConditionIdsForConditionGroupNone();
                case ConditionGroup.Attitude:
                    return GetConditionIdsForConditionGroupAttitude();
                case ConditionGroup.DeathAndDying:
                    return GetConditionIdsForConditionGroupDeathAndDying();
                case ConditionGroup.DegreesOfDetection:
                    return GetConditionIdsForConditionGroupDegreesOfDetection();
                case ConditionGroup.LoweredAbilities:
                    return GetConditionIdsForConditionGroupLoweredAbilities();
                case ConditionGroup.Senses:
                    return GetConditionIdsForConditionGroupSenses();
            }
            var msg = string.Format("Condition groul '{0}' not supported.");
            throw new System.NotSupportedException(msg);
        }

        public virtual ICondition CreateCondition(IEntity entityToApply, int conditionId, params object[] args)
        {
            return CreateCondition(entityToApply, conditionId, null, null, args);
        }

        public virtual ICondition CreateCondition(IEntity entityToApply, int conditionId, string conditionName, 
                string conditionDescription, params object[] args)
        {
            switch(conditionId)
            {
                case EntityIds.BLINDED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Blinded), entityToApply, conditionName, conditionDescription);

                case EntityIds.BROKEN_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Broken), entityToApply, conditionName, conditionDescription);

                case EntityIds.CLUMSY_CONDITION_ID:
                    var clumsyLevel = GetConditionLevelFromArgs(args);
                    return CreateConditionWithSingleIntArg(typeof(Clumsy), entityToApply, conditionName, conditionDescription, clumsyLevel);

                case EntityIds.CONCEALED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Concealed), entityToApply, conditionName, conditionDescription);

                case EntityIds.CONFUSED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Confused), entityToApply, conditionName, conditionDescription);

                case EntityIds.CONTROLLED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Controlled), entityToApply, conditionName, conditionDescription);

                case EntityIds.DAZZLED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Dazzled), entityToApply, conditionName, conditionDescription);

                case EntityIds.DEAFENED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Deafened), entityToApply, conditionName, conditionDescription);

                case EntityIds.DOOMED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Doomed), entityToApply, conditionName, conditionDescription);

                case EntityIds.DRAINED_CONDITION_ID:
                    var drainedLevel = GetConditionLevelFromArgs(args);
                    return CreateConditionWithSingleIntArg(typeof(Drained), entityToApply, conditionName, conditionDescription, drainedLevel);

                case EntityIds.DYING_CONDITION_ID:
                    var dyingLevel = GetConditionLevelFromArgs(args);
                    return CreateConditionWithSingleIntArg(typeof(Dying), entityToApply, conditionName, conditionDescription, dyingLevel);

                case EntityIds.ENCUMBERED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Encumbered), entityToApply, conditionName, conditionDescription);

                case EntityIds.ENFEEBLED_CONDITION_ID:
                    var enfeebledLevel = GetConditionLevelFromArgs(args);
                    return CreateConditionWithSingleIntArg(typeof(Enfeebled), entityToApply, conditionName, conditionDescription, enfeebledLevel);

                case EntityIds.FASCINATED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Fascinated), entityToApply, conditionName, conditionDescription);

                case EntityIds.FATIGUED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Fatigued), entityToApply, conditionName, conditionDescription);

                case EntityIds.FLAT_FOOTED_CONDITION_ID:
                    var flatFootedTo = GetIntArrayFromArgs(args);
                    return CreateFlatFootedCondition(typeof(FlatFooted), entityToApply, conditionName, conditionDescription, flatFootedTo);

                case EntityIds.FLEEING_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Fleeing), entityToApply, conditionName, conditionDescription);

                case EntityIds.FRIENDLY_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Friendly), entityToApply, conditionName, conditionDescription);

                case EntityIds.FRIGHTENED_CONDITION_ID:
                    var frightenedLevel = GetConditionLevelFromArgs(args);
                    return CreateConditionWithSingleIntArg(typeof(Frightened), entityToApply, conditionName, conditionDescription, frightenedLevel);

                case EntityIds.GRABBED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Grabbed), entityToApply, conditionName, conditionDescription);

                case EntityIds.HELPFUL_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Helpful), entityToApply, conditionName, conditionDescription);

                case EntityIds.HIDDEN_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Hidden), entityToApply, conditionName, conditionDescription);

                case EntityIds.HOSTILE_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Hostile), entityToApply, conditionName, conditionDescription);

                case EntityIds.IMMOBILIZED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Immobilized), entityToApply, conditionName, conditionDescription);

                case EntityIds.INDIFFERENT_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Indifferent), entityToApply, conditionName, conditionDescription);

                case EntityIds.INVISIBLE_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Invisible), entityToApply, conditionName, conditionDescription);

                case EntityIds.OBSERVED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Observed), entityToApply, conditionName, conditionDescription);

                case EntityIds.PERSISTENT_DAMAGE_CONDITION_ID:
                    return CreatePersistentDamageCondition(typeof(PersistentDamage), entityToApply, conditionName, conditionDescription, args);

                case EntityIds.PETRIFIED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Petrified), entityToApply, conditionName, conditionDescription);

                case EntityIds.PRONE_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Prone), entityToApply, conditionName, conditionDescription);

                case EntityIds.QUICKENED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Quickened), entityToApply, conditionName, conditionDescription);

                case EntityIds.RESTRAINED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Restrained), entityToApply, conditionName, conditionDescription);

                case EntityIds.SICKENED_CONDITION_ID:
                    var sickenedLevel = GetConditionLevelFromArgs(args);
                    return CreateConditionWithSingleIntArg(typeof(Sickened), entityToApply, conditionName, conditionDescription, sickenedLevel);

                case EntityIds.SLOWED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Slowed), entityToApply, conditionName, conditionDescription);

                case EntityIds.STUNNED_CONDITION_ID:
                    var stunnedLevel = GetConditionLevelFromArgs(args);
                    return CreateConditionWithSingleIntArg(typeof(Stunned), entityToApply, conditionName, conditionDescription, stunnedLevel);

                case EntityIds.STUPEFIED_CONDITION_ID:
                    var stupefiedLevel = GetConditionLevelFromArgs(args);
                    return CreateConditionWithSingleIntArg(typeof(Stupefied), entityToApply, conditionName, conditionDescription, stupefiedLevel);

                case EntityIds.UNCONSCIOUS_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Unconscious), entityToApply, conditionName, conditionDescription);

                case EntityIds.UNDETECTED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Undetected), entityToApply, conditionName, conditionDescription);

                case EntityIds.UNFRIENDLY_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Unfriendly), entityToApply, conditionName, conditionDescription);

                case EntityIds.UNNOTICED_CONDITION_ID:
                    return CreateConditionWithoutArgs(typeof(Unnoticed), entityToApply, conditionName, conditionDescription);

                case EntityIds.WOUNDED_CONDITION_ID:
                    var woundedLevel = GetConditionLevelFromArgs(args);
                    return CreateConditionWithSingleIntArg(typeof(Wounded), entityToApply, conditionName, conditionDescription, woundedLevel);

                default:
                    var msg = string.Format("Condition ID {0} does not exist.", conditionId);
                    throw new System.ArgumentOutOfRangeException(msg);
            }
        }

        #region Get condition IDs by group name

        protected List<int> GetConditionIdsForConditionGroupNone()
        {
            var ids = new List<int>();
            ids.Add(EntityIds.BROKEN_CONDITION_ID);
            ids.Add(EntityIds.CONFUSED_CONDITION_ID);
            ids.Add(EntityIds.CONTROLLED_CONDITION_ID);
            ids.Add(EntityIds.ENCUMBERED_CONDITION_ID);
            ids.Add(EntityIds.FASCINATED_CONDITION_ID);
            ids.Add(EntityIds.FATIGUED_CONDITION_ID);
            ids.Add(EntityIds.FLAT_FOOTED_CONDITION_ID);
            ids.Add(EntityIds.FLEEING_CONDITION_ID);
            ids.Add(EntityIds.FRIGHTENED_CONDITION_ID);
            ids.Add(EntityIds.GRABBED_CONDITION_ID);
            ids.Add(EntityIds.IMMOBILIZED_CONDITION_ID);
            ids.Add(EntityIds.PERSISTENT_DAMAGE_CONDITION_ID);
            ids.Add(EntityIds.PETRIFIED_CONDITION_ID);
            ids.Add(EntityIds.PRONE_CONDITION_ID);
            ids.Add(EntityIds.QUICKENED_CONDITION_ID);
            ids.Add(EntityIds.RESTRAINED_CONDITION_ID);
            ids.Add(EntityIds.SICKENED_CONDITION_ID);
            ids.Add(EntityIds.STUNNED_CONDITION_ID);
            return ids;
        }

        protected List<int> GetConditionIdsForConditionGroupAttitude()
        {
            var ids = new List<int>();
            ids.Add(EntityIds.FRIENDLY_CONDITION_ID);
            ids.Add(EntityIds.HELPFUL_CONDITION_ID);
            ids.Add(EntityIds.HOSTILE_CONDITION_ID);
            ids.Add(EntityIds.INDIFFERENT_CONDITION_ID);
            ids.Add(EntityIds.UNFRIENDLY_CONDITION_ID);
            return ids;
        }

        protected List<int> GetConditionIdsForConditionGroupDeathAndDying()
        {
            var ids = new List<int>();
            ids.Add(EntityIds.DOOMED_CONDITION_ID);
            ids.Add(EntityIds.DYING_CONDITION_ID);
            ids.Add(EntityIds.UNCONSCIOUS_CONDITION_ID);
            ids.Add(EntityIds.WOUNDED_CONDITION_ID);
            return ids;
        }

        protected List<int> GetConditionIdsForConditionGroupDegreesOfDetection()
        {
            var ids = new List<int>();
            ids.Add(EntityIds.HIDDEN_CONDITION_ID);
            ids.Add(EntityIds.OBSERVED_CONDITION_ID);
            ids.Add(EntityIds.UNDETECTED_CONDITION_ID);
            ids.Add(EntityIds.UNNOTICED_CONDITION_ID);
            return ids;
        }
        
        protected List<int> GetConditionIdsForConditionGroupLoweredAbilities()
        {
            var ids = new List<int>();
            ids.Add(EntityIds.CLUMSY_CONDITION_ID);
            ids.Add(EntityIds.DRAINED_CONDITION_ID);
            ids.Add(EntityIds.ENFEEBLED_CONDITION_ID);
            ids.Add(EntityIds.STUPEFIED_CONDITION_ID);;
            return ids;
        }

        protected List<int> GetConditionIdsForConditionGroupSenses()
        {
            var ids = new List<int>();
            ids.Add(EntityIds.BLINDED_CONDITION_ID);
            ids.Add(EntityIds.CONCEALED_CONDITION_ID);
            ids.Add(EntityIds.DAZZLED_CONDITION_ID);
            ids.Add(EntityIds.DEAFENED_CONDITION_ID);
            ids.Add(EntityIds.INVISIBLE_CONDITION_ID);
            return ids;
        }

        #endregion

        #region Create condition by condition ID

        protected ICondition CreateConditionWithoutArgs(Type conditionType, IEntity entity, string name, string description)
        {
            ICondition condition;
            if(string.IsNullOrEmpty(name))
            {
                condition = (ICondition)Activator.CreateInstance(conditionType);
            }
            else
            {
                condition = (ICondition)Activator.CreateInstance(conditionType, name, description);
            }
            condition.Apply(entity);
            return condition;
        }

        protected ICondition CreateConditionWithSingleIntArg(Type conditionType, IEntity entity, string name, string description, int level)
        {
            ICondition condition;
            if(string.IsNullOrEmpty(name))
            {
                condition = (ICondition)Activator.CreateInstance(conditionType, level);
            }
            else
            {
                condition = (ICondition)Activator.CreateInstance(conditionType, level, name, description);
            }
            condition.Apply(entity);
            return condition;
        }

        protected ICondition CreateFlatFootedCondition(Type conditionType, IEntity entity, string name, string description, int[] flatFootedToo)
        {
            ICondition condition;
            if(string.IsNullOrEmpty(name))
            {
                condition = (ICondition)Activator.CreateInstance(conditionType, flatFootedToo);
            }
            else
            {
                condition = (ICondition)Activator.CreateInstance(conditionType, name, description, flatFootedToo);
            }
            condition.Apply(entity);
            return condition;
        }

        protected ICondition CreatePersistentDamageCondition(Type conditionType, IEntity entity, string name, string description, object[] args)
        {
            if(args.Length < 2)
            {
                throw new ArgumentOutOfRangeException("Must include damage type (as a string) and Die object in arguments.");
            }
            string damageType = args[0].ToString();
            var die = (Die)args[1];

            ICondition condition;
            if(string.IsNullOrEmpty(name))
            {
                condition = (ICondition)Activator.CreateInstance(conditionType, damageType, die);
            }
            else
            {
                condition = (ICondition)Activator.CreateInstance(conditionType, name, description, damageType, die);
            }
            condition.Apply(entity);
            return condition;

        }

        protected int GetConditionLevelFromArgs(object[] args)
        {
            var level = 1;
            if(args.Length > 0 && int.TryParse(args[0].ToString(), out var buffer))
            {
                level = buffer;
            }
            return level;
        }

        protected int[] GetIntArrayFromArgs(object[] args)
        {
            var buffer = new List<int>();
            foreach(var obj in args)
            {
                if(int.TryParse(obj.ToString(), out var tempInt))
                {
                    buffer.Add(tempInt);
                }
            }
            return buffer.ToArray();
        }

        #endregion
    }
}