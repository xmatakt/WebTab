  select ColumnName, ColumnOrder, e.Effect, IsActive from ColumnInfo as I
  inner join ColumnEffect as e
  on I.ColumnEffect = e.EffectID