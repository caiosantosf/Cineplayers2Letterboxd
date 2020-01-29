module.exports = (sequelize, Sequelize) => {
  const Movie = sequelize.define("movies", {
    title: {type: Sequelize.STRING},
    year: {type: Sequelize.INTEGER},
    minutes: {type: Sequelize.INTEGER},
    publishersAverage: {type: Sequelize.INTEGER},
    usersAverage: {type: Sequelize.INTEGER}
  })

  return Movie
}