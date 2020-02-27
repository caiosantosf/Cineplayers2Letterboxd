module.exports = (sequelize, Sequelize) => {
    const Country = sequelize.define("countries", {
      name: {type: Sequelize.STRING}
    })

    Country.associate = models => {
      models.countries.belongsToMany(models.movies, {
        through: 'movieCountries',
        as: 'movies',
        foreignKey: 'movieId',
        otherKey: 'countryId'
      })
    }
  
    return Country
  }