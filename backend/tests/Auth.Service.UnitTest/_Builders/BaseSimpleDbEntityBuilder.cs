using Auth.Service.Domain.Entities.General;

namespace Auth.Service.UnitTest._Builders;

public abstract class BaseSimpleDbEntityBuilder<TBuilder, TConcrete>
        where TBuilder : new()
        where TConcrete : SimpleDbEntity
{
    protected int id;

    public static TBuilder New()
    {
        return new TBuilder();
    }

    public TBuilder WithId(int value)
    {
        id = value;

        return GetThis();
    }

    protected abstract TBuilder GetThis();

    public abstract TConcrete Build();

    protected TConcrete Build(TConcrete model)
    {
        if (model is null)
            return model;

        model.Id = id;

        return model;
    }
}
