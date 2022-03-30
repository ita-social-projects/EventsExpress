namespace EventsExpress.Test.GraphQLTests
{
    internal static class TestQueries
    {
        internal static string GetQuery()
        {
            return @"
            {
              events {
                nodes {
                  id
                  title
                  description
                  dateFrom
                  dateTo
                  isPublic
                  maxParticipants
                  visitors {
                    user {
                      id
                    }
                  }
                  categories {
                    category {
                      id
                    }
                  }
                  rates {
                    id
                  }
                  inventories {
                    id
                  }
                  statusHistory {
                    id
                  }
                  location {
                    id
                    point {
                      x
                      y
                      sRID
                    }
                    onlineMeeting
                  }
                  eventSchedule {
                    id
                  }
                  eventAudience {
                    id
                  }
                }
              }
            }
            ";
        }

        internal static string GetQueryWithFilterByCategoryName()
        {
            return @"
            {
              events(
                where: {
                  categories: {
                    some: {
                      category: {
                        name: {
                          contains: ""Second""
                        }
                      }
                    }
                  }
                }
            ) {
                nodes {
                  title
                  categories {
                    category {
                      id
                      name
                    }
                  }
                }
              }
            }
            ";
        }

        internal static string GetQueryWithFilterByLocationCoordinates()
        {
            return @"
            {
              events(
                first: 50
                where: {
                  location: {
                    point: {
                      distance: {
                        geometry: {
                          type: Point
                          coordinates: [48.248, 26.012]
                          crs: 4326
                        }
                        lte: 500
                      }
                    }
                  }
                }
              ) {
                nodes {
                  title
                  location {
                    point {
                      x
                      y
                      sRID
                    }
                  }

                }
              }
            }
            ";
        }

        internal static string GetQueryWithPagingFilter()
        {
            return @"
            {
              events(
                first: 2
              ) {
                nodes {
                  title
                  description
                }
              }
            }
            ";
        }
    }
}
