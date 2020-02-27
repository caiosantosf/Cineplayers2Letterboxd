module.exports = (sequelize, Sequelize) => {
  const Movie = sequelize.define("movies", {
    title: {type: Sequelize.STRING},
    year: {type: Sequelize.INTEGER},
    minutes: {type: Sequelize.INTEGER},
    publishersAverage: {type: Sequelize.INTEGER},
    usersAverage: {type: Sequelize.INTEGER}
  })

  Movie.associate = models => {
    models.movies.belongsToMany(models.countries, {
      through: 'movieCountries',
      as: 'countries',
      foreignKey: 'movieId',
      otherKey: 'countryId'
    })

    models.movies.belongsToMany(models.people, {
      through: 'moviePeople',
      as: 'people',
      foreignKey: 'movieId',
      otherKey: 'personId'
    })
  }

  return Movie
}